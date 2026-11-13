// Extract what a phone reader sees (390px, folds closed) on the Summary, Options and Arguments tabs.
const { chromium } = require('playwright');
(async () => {
  const file = 'file://' + process.argv[2], slug = process.argv[3], out = process.argv[4];
  const browser = await chromium.launch();
  const page = await browser.newPage({ viewport: { width: 390, height: 844 } });
  let text = '';
  for (const [tab, name] of [['', 'Summary'], ['-options', 'Options'], ['-arguments', 'Arguments']]) {
    await page.goto(file + '#' + slug + tab, { waitUntil: 'load' });
    await page.waitForTimeout(200);
    const t = await page.evaluate(() => document.getElementById('app').innerText);
    text += `\n\n===== ${name} tab (what shows before tapping anything) =====\n` + t;
  }
  await page.goto(file + '#home', { waitUntil: 'load' }); await page.waitForTimeout(200);
  const home = await page.evaluate(() => document.getElementById('app').innerText);
  require('fs').writeFileSync(out, '===== Home page =====\n' + home + text);
  await browser.close();
})();
