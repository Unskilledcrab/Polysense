// Phone screenshots of chosen routes, for review. Usage: node prototypes/tests/shots.js /abs/index.html OUT_DIR route[:height] ...
const { chromium } = require('playwright');
(async () => {
  const [file, out, ...routes] = process.argv.slice(2);
  const browser = await chromium.launch();
  for (const spec of routes) {
    const [route, h] = spec.split(':');
    const page = await browser.newPage({ viewport: { width: 390, height: Number(h || 1700) }, deviceScaleFactor: 1 });
    await page.goto('file://' + file + '#' + route, { waitUntil: 'load' });
    await page.waitForTimeout(400);
    await page.screenshot({ path: `${out}/${route}.png` });
    await page.close();
  }
  await browser.close();
})();
