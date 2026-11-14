// Prototype checks: every screen at phone and desktop width, then content and app-behavior checks. Usage: node prototypes/tests/smoke.js /abs/path/index.html
const { chromium } = require('playwright');
const { serve, settle } = require('./serve');
(async () => {
  const site = await serve(process.argv[2]);   // the app fetches each decision's data, so it is served over http
  const file = site.url;
  const browser = await chromium.launch();
  const fails = [];
  const ok = (cond, msg) => { if (!cond) fails.push(msg); };
  // The decisions come from the app's own data, so the tests keep working as pages are added.
  const src = require('fs').readFileSync(process.argv[2], 'utf8');
  const D = JSON.parse(src.match(/<script type="application\/json" id="data">([\s\S]*?)<\/script>/)[1]);
  const slugs = D.workspaces.map(w => w.slug);
  const kindOf = Object.fromEntries(D.workspaces.map(w => [w.slug, w.kind]));
  const tabs = ['', '-decision', '-options', '-arguments', '-evidence', '-checks'];
  const routes = ['home', 'decisions', 'dates', 'checks'].concat(...slugs.map(s => tabs.map(t => s + t)));
  for (const width of [390, 1280]) {
    const page = await browser.newPage({ viewport: { width, height: 900 }, reducedMotion: 'reduce' });
    const errors = [], outside = [];
    page.on('request', q => { if (!q.url().startsWith(site.origin) && !/^(data|blob):/.test(q.url())) outside.push(q.url()); });
    page.on('pageerror', e => errors.push(e.message));
    page.on('console', m => { if (m.type() === 'error') errors.push(m.text()); });
    for (const r of routes) {
      await page.goto(file + '#' + r, { waitUntil: 'load' });
      await page.evaluate(h => { location.hash = h; }, r);
      await page.waitForTimeout(120);
      await settle(page);
      await page.evaluate(() => document.querySelectorAll('details').forEach(d => d.open = true));
      await page.waitForTimeout(60);
      const res = await page.evaluate(() => ({
        over: document.documentElement.scrollWidth - document.documentElement.clientWidth,
        text: document.getElementById('app').innerText,
      }));
      ok(res.over <= 0, `w=${width} #${r}: horizontal overflow ${res.over}px with all folds open`);
      ok(res.text.length > 200, `w=${width} #${r}: almost empty (${res.text.length} chars)`);
      ok(!res.text.includes('**'), `w=${width} #${r}: literal ** visible`);
      ok(!/\btrial\b/i.test(res.text.replace(/\bTrial\s+(court|courts)\b/gi, '')) || r.endsWith('-checks') || r === 'checks',
         `w=${width} #${r}: the word "trial" in reader-facing text`);
    }
    ok(errors.length === 0, `w=${width}: script errors: ${errors.join(' || ')}`);
    ok(outside.length === 0, `w=${width}: the page asked another host for ${outside.slice(0, 3).join(', ')}`);
    await page.close();
  }
  const page = await browser.newPage({ viewport: { width: 390, height: 844 }, reducedMotion: 'reduce', hasTouch: true });
  const asked = [];   // which data files the app asked for
  page.on('request', q => { const m = q.url().match(/\/data\/([^/]+)\.json$/); if (m) asked.push(m[1]); });
  const get = async (r) => { await page.goto(file + '#' + r, { waitUntil: 'load' }); await page.waitForTimeout(150); await settle(page); };
  // The app holds only what Home needs; a decision's full text loads when it is opened, and only that one.
  await get('home');
  ok(asked.length === 0, `Home asked for decision data: ${asked.join(', ')}`);
  ok(await page.evaluate(() => JSON.parse(document.getElementById('data').textContent).workspaces.every(w => w.lite && w.workspace.sections.length <= 1)),
     'the app file carries a decision\'s full text');
  await get(slugs[1]);
  ok(asked.join(',') === slugs[1], `opening ${slugs[1]} asked for: ${asked.join(', ') || 'nothing'}`);
  ok(await page.$('.tabs') !== null && await page.$('#loadnote') === null, `${slugs[1]} did not finish loading`);
  // If the data can't be fetched, the reader is told and can try again.
  await page.route('**/data/*.json', route => route.abort());
  await get(slugs[2]);
  ok(/couldn't be loaded/.test(await page.innerText('#app')) && await page.$('#loadnote button') !== null, 'a failed load shows no message or retry');
  await page.unroute('**/data/*.json');
  await page.click('#loadnote button'); await page.waitForTimeout(200); await settle(page);
  ok(await page.$('.tabs') !== null, '"Try again" did not load the decision');
  // Headings follow the kind of decision.
  for (const s of slugs) {
    await get(s + '-decision'); const h = await page.innerText('.layer-head h2');
    if (kindOf[s] === 'law') { ok(h === 'What the law does', `${s} decision heading is "${h}"`); continue; }
    ok(h === "What's being decided", `${s} decision heading is "${h}"`);
    await get(s + '-options'); const lede = await page.innerText('.layer-head .lede'); ok(!/lawmakers/i.test(lede), `${s} options lede mentions lawmakers`);
  }
  // Summary: "What it means for you" card present on every page; no Decide chip on the PSC page.
  for (const s of slugs) {
    await get(s); ok(await page.$('section.you') !== null, `${s}: no "What it means for you" card`);
    const ifs = await page.$$eval('section.you .ifyou strong.lead', n => n.map(x => x.innerText));
    ok(ifs.length >= 2 && ifs.every(t => /^If (you|your)\b/i.test(t)), `${s}: "If you…" lines missing or mislabeled (${ifs.join(' | ')})`);
  }
  await get('psc-57171');
  // "Decide" is the reader's power; officials go under "Who decides". Check chips and field labels on every page.
  for (const s of slugs) {
    await get(s);
    // The actions live in the "What you can do" sheet, opened by the pinned button.
    ok(await page.isVisible('#actbtn'), `${s}: no pinned "What you can do" button`);
    await page.click('#actbtn'); await page.waitForTimeout(120);
    ok((await page.$$('.drawer .acard')).length >= 1, `${s}: "What you can do" opened no actions`);
    const labels = await page.$$eval('.power, .flabel, .label', n => n.map(x => x.innerText.trim()));
    ok(!labels.some(t => /^decide:?$/i.test(t)), `${s}: a Decide label on the Summary tab`);
    ok(await page.$('.power.decide') === null, `${s}: a "You vote" chip, but no page here is a direct vote by the reader`);
    const chips = await page.$$eval('.power', n => n.map(x => x.innerText.trim()));
    ok(chips.length >= 1 && chips.every(t => /^(Have your say|Watch or read|You vote)$/i.test(t)), `${s}: power chip not in plain words (${chips.join(', ')})`);
    await page.keyboard.press('Escape');
  }
  // Options: values guide with one line per option card.
  for (const s of slugs) {
    await get(s + '-options');
    // Count only the "If you care most about…" lines; notes (shared values, sides without a line) are allowed too.
    const v = await page.$$eval('.vguide .vline', n => n.filter(x => /^If you care most about/i.test(x.innerText.trim())).length), cards = await page.$$eval('.options > details.fold', n => n.length);
    ok(v >= 1, `${s}: no values guide`);
    ok(v <= cards + 1, `${s}: ${v} values lines for ${cards} options`);
    const vt = await page.$$eval('.vguide .vline', n => n.map(x => x.innerText).join(' '));
    ok(!/\b(fits?|best|should choose|choose this)\b/i.test(vt.replace(/"[^"]*"|“[^”]*”/g, '')), `${s}: recommendation word in the values guide (outside quotes)`);
  }
  // Options keep their letters (the values guide names "Option C"); the money steps are open by default.
  for (const s of slugs) {
    await get(s + '-options');
    const titles = await page.$$eval('.options > details.fold > summary h3', n => n.map(x => x.innerText));
    ok(titles.length === 0 || titles.every(t => /^(Option|Plan|Outcome) [A-Z]: /.test(t)), `${s}: option titles without letters (${titles.join(' | ')})`);
    const named = await page.$$eval('.vguide .vline', n => n.map(x => x.innerText).join(' ').match(/\b(Option|Plan) [A-Z]\b/g) || []);
    named.forEach(k => ok(titles.some(t => t.startsWith(k + ':')), `${s}: values guide names "${k}", but no option card is titled that way`));
    await get(s);
    // Each Summary row opens a sheet with content and no sideways scroll; the money trail is drawn as steps.
    const rows = await page.$$('.layer .list button.row');
    ok(rows.length >= 2, `${s}: only ${rows.length} rows under the Summary`);
    for (const row of rows) {
      const name = (await row.innerText()).trim().split('\n')[0];
      await row.click(); await page.waitForTimeout(120);
      const sheet = await page.evaluate(() => { const d = document.querySelector('.drawer .sbody');
        return d ? { len: d.innerText.length, over: document.documentElement.scrollWidth - document.documentElement.clientWidth, inner: d.scrollWidth - d.clientWidth,
          steps: d.querySelectorAll('ol.flow li').length } : null; });
      ok(sheet && sheet.len > 40, `${s}: the "${name}" row opened an empty sheet`);
      ok(sheet && sheet.over <= 0 && sheet.inner <= 0, `${s}: the "${name}" sheet scrolls sideways`);
      if (/where the money/i.test(name)) ok(sheet && sheet.steps >= 2, `${s}: the money trail isn't drawn as steps`);
      await page.keyboard.press('Escape');
      ok(await page.$('.drawer') === null, `${s}: Escape didn't close the "${name}" sheet`);
    }
    // "Not known yet" is always in view: its own card, never folded or behind a tap.
    const unknown = await page.$$eval('section.unknown', n => n.map(x => !x.closest('details') && x.innerText.length > 40));
    ok(unknown.length === 1 && unknown[0], `${s}: "Not known yet" missing or hidden`);
    // A source dot opens the exact passage; tapping outside the sheet closes it.
    await page.click('.layer .src:not(.tag)'); await page.waitForTimeout(120);
    ok(await page.$('.drawer blockquote') !== null, `${s}: a source dot didn't open a quoted passage`);
    await page.mouse.click(195, 40); await page.waitForTimeout(80);
    ok(await page.$('.drawer') === null, `${s}: tapping outside didn't close the sheet`);
    const dots = await page.$$eval('.layer .src', n => n.map(x => (x.getAttribute('aria-label') || '').length));
    ok(dots.length > 3 && dots.every(l => l > 8), `${s}: a source dot has no spoken label`);
    const head = await page.innerText('.billhead');
    ok(!/fingerprint|round \d/i.test(head), `${s}: fingerprint or round number in the page header`);
  }
  // Reader-first visuals: a countdown badge; "pick your situation" swaps the line shown; pictures have text equivalents.
  for (const s of slugs) {
    await get(s);
    ok(await page.$('.billhead .datebadge') !== null, `${s}: no date badge`);
    const btns = await page.$$('section.you .pick button');
    ok(btns.length >= 2, `${s}: situation picker has ${btns.length} choices`);
    if (btns.length >= 2) {
      await btns[1].click();
      const shown = await page.$$eval('section.you .ifyou', n => n.map(x => !x.hidden));
      ok(shown.filter(Boolean).length === 1 && shown[1], `${s}: picking the second situation didn't show only its line`);
    }
    const imgs = await page.$$eval('.viz [role="img"]', n => n.map(x => x.getAttribute('aria-label') || ''));
    ok(imgs.every(t => t.length > 8), `${s}: a picture has no text equivalent`);
  }
  for (const s of ['psc-57171', 'fulton-budget-2027', 'psc-57243'].filter(x => slugs.includes(x))) { await get(s); ok(await page.$('.viz') !== null, `${s}: no "At a glance" card`); }
  ok(!/localStorage|sessionStorage|indexedDB|document\.cookie/.test(src), 'the page stores something on the device');
  // "How we checked" must agree with the badge: a page that passed can't also say its changes weren't re-checked.
  for (const s of slugs) {
    await get(s + '-checks');
    const t = await page.innerText('#app');
    const passed = /Passed our checks/.test(await page.innerText('.chips'));
    ok(!(passed && /not been re-checked|not finished|waiting for a check/i.test(t)), `${s}: "How we checked" says the page isn't finished, beside a "Passed" badge`);
    ok(/current check|waiting for a check|check is failing/.test(t), `${s}: no generated line on whether the page's checks are current`);
  }
  // Arguments: legend present; Mixed and Uncertain differ; the legend button leaves its card folded.
  for (const s of slugs) {
    await get(s + '-arguments');
    ok(await page.$('.legend') !== null, `${s}: no legend`);
    const btn = await page.$('details.fold .evbtn');
    if (btn) { await btn.click(); await page.waitForTimeout(120);
      ok(await page.$('.drawer .legend') !== null, `${s}: legend button didn't open the legend`);
      ok((await page.$$eval('details.fold[open]', n => n.length)) === 0, `${s}: legend button unfolded a card`);
      await page.keyboard.press('Escape'); }
  }
  // A card whose label names its parts shows one line per grade with those parts (lay readers couldn't tell which grade went with which part).
  await get('fulton-budget-2027-arguments');
  const parts = await page.$$eval('details.fold > summary .evpart .evlist li', n => n.map(x => x.textContent));
  ok(parts.length >= 6 && parts.every(t => /^\S+.*:\s*\S/.test(t)), `fulton: per-part evidence lines missing (${parts.length})`);
  await get('psc-57171-arguments');
  const styles = await page.evaluate(() => {
    const pick = c => { const e = document.querySelector('.ev.' + c); return e ? getComputedStyle(e).color + '|' + getComputedStyle(e).borderTopStyle + '|' + e.innerText.trim()[0] : null; };
    return { mixed: pick('mixed'), uncertain: pick('uncertain') };
  });
  ok(!styles.mixed || !styles.uncertain || styles.mixed !== styles.uncertain, `Mixed and Uncertain look the same: ${JSON.stringify(styles)}`);
  // Home: decisions first, one card each; then ways to weigh in. No address field.
  await get('home');
  ok((await page.innerText('h1')) === 'Decisions', 'home title');
  ok((await page.$$('.dcard')).length === slugs.length, 'home: one card per decision');
  const h2 = await page.$$eval('.home-sec > h2', n => n.map(x => x.childNodes[0].textContent.trim()));
  ok(h2.includes('If you want to weigh in') && !h2.includes('Act now'), `home sections: ${h2.join(', ')}`);
  ok(h2[0] === 'Being decided' && h2.includes('Decided'), `home groups: ${h2.join(', ')}`);
  // The topic filter appears only when the list is long enough to need it; then a chip narrows the cards.
  const chips = await page.$$('.filter button');
  ok((slugs.length > 3) === (chips.length > 0), `topic filter with ${slugs.length} decisions: ${chips.length} chips`);
  if (chips.length > 1) {
    await chips[1].click(); await page.waitForTimeout(120);
    const left = await page.$$eval('.dcard .tlabel', n => [...new Set(n.map(x => x.innerText))]);
    ok(left.length === 1, `one topic chosen, but cards show: ${left.join(', ')}`);
    await page.click('.filter button'); await page.waitForTimeout(120);
  }
  ok(await page.$('input') === null, 'home has an input field');
  // The tab bar: three items, each with an icon and a label, each tall enough to tap.
  const tabbar = await page.$$eval('.bottom a', n => n.map(a => ({ t: a.innerText.trim(), svg: !!a.querySelector('svg'), h: a.getBoundingClientRect().height })));
  ok(tabbar.length === 3 && tabbar.every(a => a.t && a.svg && a.h >= 44), `tab bar: ${JSON.stringify(tabbar)}`);
  // Tapping a card opens its decision; the back button returns Home.
  await page.click('.dcard'); await page.waitForTimeout(150); await settle(page);
  ok(slugs.includes(await page.evaluate(() => location.hash.slice(1))), 'tapping a home card did not open a decision');
  ok(await page.$('.billhead') !== null, 'no decision header after tapping a card');
  await page.click('.back'); await page.waitForTimeout(150);
  ok((await page.evaluate(() => location.hash)) === '#home', 'the back button did not return Home');
  // After going in and back, Home is laid out at the screen's width (a class clash once made it thousands of pixels wide).
  const wide = await page.evaluate(() => Math.round(document.querySelector('.dcard').getBoundingClientRect().width));
  ok(wide <= 390, `Home cards are ${wide}px wide after going back`);
  // A source dot inside a home card opens its passage and leaves the reader on Home.
  await page.click('.dcard .src'); await page.waitForTimeout(120);
  ok(await page.$('.drawer blockquote') !== null && (await page.evaluate(() => location.hash)) === '#home', 'a source dot on a home card navigated away or showed nothing');
  await page.keyboard.press('Escape');
  // Dated lists: ascending, with "What happened" newest first.
  const lists = async () => page.$$eval('.home-sec', secs => secs.filter(s => s.querySelector(':scope > h2')).map(s => ({ t: s.querySelector(':scope > h2').childNodes[0].textContent.trim(),
    d: [...s.querySelectorAll('.when')].map(w => w.innerText) })));
  const order = await lists();
  await get('dates');
  ok((await page.innerText('h1')) === 'Dates', 'dates title');
  const order2 = await lists();
  ok(order2.length >= 2 && order2.every(x => x.d.length >= 1), `dates lists: ${JSON.stringify(order2)}`);
  console.log('dated lists:', JSON.stringify(order.concat(order2)));
  await browser.close();
  site.close();
  if (fails.length) { console.log('FAIL (' + fails.length + ')'); fails.forEach(f => console.log(' - ' + f)); process.exit(1); }
  console.log('PASS: ' + routes.length + ' routes x 2 widths, plus content checks');
})();
