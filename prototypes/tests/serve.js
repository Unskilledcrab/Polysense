// A small local web server for the tests: the app loads each decision's data with fetch(), which needs http.
// Usage: const { serve } = require('./serve'); const { url, close } = await serve('/abs/path/index.html');
const http = require('http'), fs = require('fs'), path = require('path');
const TYPES = { '.html': 'text/html; charset=utf-8', '.json': 'application/json; charset=utf-8', '.js': 'text/javascript', '.css': 'text/css' };
exports.serve = async (indexPath) => {
  const root = path.dirname(path.resolve(indexPath));
  const server = http.createServer((req, res) => {
    const rel = decodeURIComponent(req.url.split('?')[0]);
    const f = path.join(root, path.normalize(rel));
    if (!f.startsWith(root + path.sep) || !fs.existsSync(f) || fs.statSync(f).isDirectory()) { res.writeHead(404); res.end('not found'); return; }
    res.writeHead(200, { 'content-type': TYPES[path.extname(f)] || 'application/octet-stream', 'cache-control': 'no-store' });
    fs.createReadStream(f).pipe(res);
  });
  await new Promise(r => server.listen(0, '127.0.0.1', r));
  const origin = `http://127.0.0.1:${server.address().port}`;
  return { origin, url: `${origin}/${path.basename(indexPath)}`, close: () => server.close() };
};
// On every screen: wait until a decision that is loading has loaded.
exports.settle = (page) => page.waitForFunction(() => !document.getElementById('loadnote'), null, { timeout: 5000 }).catch(() => {});
