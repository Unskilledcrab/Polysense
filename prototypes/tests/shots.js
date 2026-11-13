// Phone screenshots of chosen routes, for review.
// Usage: node prototypes/tests/shots.js /abs/index.html OUT_DIR SPEC ...
// SPEC is route[:height][@dark][!selector]: "!selector" clicks that element after the screen loads (to open a sheet).
const { chromium } = require('playwright');
(async () => {
  const [file, out, ...specs] = process.argv.slice(2);
  const browser = await chromium.launch();
  for (let spec of specs) {
    let click = null, dark = false;
    if (spec.includes('!')) { [spec, click] = [spec.slice(0, spec.indexOf('!')), spec.slice(spec.indexOf('!') + 1)]; }
    if (spec.endsWith('@dark')) { dark = true; spec = spec.slice(0, -5); }
    const [route, h] = spec.split(':');
    const page = await browser.newPage({ viewport: { width: 390, height: Number(h || 1700) }, deviceScaleFactor: 1,
      colorScheme: dark ? 'dark' : 'light', reducedMotion: 'reduce', hasTouch: true });
    await page.goto('file://' + file + '#' + route, { waitUntil: 'load' });
    await page.waitForTimeout(300);
    if (click) { await page.click(click); await page.waitForTimeout(300); }
    await page.screenshot({ path: `${out}/${route}${dark ? '-dark' : ''}${click ? '-sheet' : ''}.png` });
    await page.close();
  }
  await browser.close();
})();
