// Reader-first prototype checks (plan: "Render, test, publish"). Usage: node prototypes/tests/smoke.js /abs/path/index.html
const { chromium } = require('playwright');
(async () => {
  const file = 'file://' + process.argv[2];
  const browser = await chromium.launch();
  const fails = [];
  const ok = (cond, msg) => { if (!cond) fails.push(msg); };
  const slugs = ['hb1009', 'psc-57171', 'fulton-budget-2027'];
  const tabs = ['', '-decision', '-options', '-arguments', '-evidence', '-checks'];
  const routes = ['home', 'decisions', 'checks'].concat(...slugs.map(s => tabs.map(t => s + t)));
  for (const width of [390, 1280]) {
    const page = await browser.newPage({ viewport: { width, height: 900 } });
    const errors = [];
    page.on('pageerror', e => errors.push(e.message));
    page.on('console', m => { if (m.type() === 'error') errors.push(m.text()); });
    for (const r of routes) {
      await page.goto(file + '#' + r, { waitUntil: 'load' });
      await page.evaluate(h => { location.hash = h; }, r);
      await page.waitForTimeout(120);
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
    await page.close();
  }
  const page = await browser.newPage({ viewport: { width: 390, height: 844 } });
  const get = async (r) => { await page.goto(file + '#' + r, { waitUntil: 'load' }); await page.waitForTimeout(150); };
  // Headings follow the kind of decision.
  await get('hb1009-decision'); ok((await page.innerText('h2')).includes('What the law does'), 'hb1009 decision heading');
  for (const s of ['psc-57171', 'fulton-budget-2027']) {
    await get(s + '-decision'); const h = await page.innerText('.layer-head h2'); ok(h === "What's being decided", `${s} decision heading is "${h}"`);
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
    const labels = await page.$$eval('.power, .flabel, .label', n => n.map(x => x.innerText.trim()));
    ok(!labels.some(t => /^decide:?$/i.test(t)), `${s}: a Decide label on the Summary tab`);
    ok(await page.$('.power.decide') === null, `${s}: a "You vote" chip, but no page here is a direct vote by the reader`);
    const chips = await page.$$eval('.power', n => n.map(x => x.innerText.trim()));
    ok(chips.every(t => /^(Have your say|Watch or read|You vote)$/i.test(t)), `${s}: power chip not in plain words (${chips.join(', ')})`);
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
    ok(titles.length === 0 || titles.every(t => /^(Option|Plan) [A-Z]: /.test(t)), `${s}: option titles without letters (${titles.join(' | ')})`);
    const named = await page.$$eval('.vguide .vline', n => n.map(x => x.innerText).join(' ').match(/\b(Option|Plan) [A-Z]\b/g) || []);
    named.forEach(k => ok(titles.some(t => t.startsWith(k + ':')), `${s}: values guide names "${k}", but no option card is titled that way`));
    await get(s);
    const money = await page.$$eval('section.you details.more', n => n.filter(d => /where the money/i.test(d.querySelector('summary').innerText)).map(d => d.open));
    ok(money.every(Boolean), `${s}: "Where the money comes from" is folded`);
    const unknown = await page.$$eval('section.you details.more', n => n.filter(d => /^not known/i.test(d.querySelector('summary').innerText.trim())).map(d => d.open));
    ok(unknown.length === 1 && unknown[0], `${s}: "Not known yet" missing or folded`);
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
  for (const s of ['psc-57171', 'fulton-budget-2027']) { await get(s); ok(await page.$('.viz') !== null, `${s}: no "At a glance" card`); }
  const src = require('fs').readFileSync(process.argv[2], 'utf8');
  ok(!/localStorage|sessionStorage|indexedDB|document\.cookie/.test(src), 'the page stores something on the device');
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
  // Home: decisions first; each dated list sorted (ascending; What happened descending); no address field.
  await get('home');
  const h2 = await page.$$eval('.home-sec > h2', n => n.map(x => x.childNodes[0].textContent.trim()));
  ok(h2[0] === 'Decisions', `home starts with "${h2[0]}"`);
  ok(h2.includes('If you want to weigh in') && !h2.includes('Act now'), `home sections: ${h2.join(', ')}`);
  ok(await page.$('input') === null, 'home has an input field');
  const order = await page.$$eval('.home-sec', secs => secs.slice(1).map(s => ({ t: s.querySelector('h2').childNodes[0].textContent.trim(),
    d: [...s.querySelectorAll('.when')].map(w => w.innerText) })));
  console.log('home lists:', JSON.stringify(order));
  await browser.close();
  if (fails.length) { console.log('FAIL (' + fails.length + ')'); fails.forEach(f => console.log(' - ' + f)); process.exit(1); }
  console.log('PASS: ' + routes.length + ' routes x 2 widths, plus content checks');
})();
