importScripts(
    'https://storage.googleapis.com/workbox-cdn/releases/6.4.1/workbox-sw.js'
);

workbox.setConfig({ debug: true });
const { strategies, routing } = workbox;

const pathEndsWith = (url, ext) => url?.pathname?.endsWith(ext);
const hostContains = (url, ext) => url?.host?.includes(ext);
const isGet = (request) => request.method === 'GET';

const isPerlaResouce = url => url?.pathname.includes("~perla~");
const isPerlaDependency =
    url =>
        hostContains(url, "cdn.skypack.dev")
        || hostContains(url, "cdn.jsdelivr.net")
        || hostContains(url, "ga.jspm.io")
        || hostContains(url, "unkpg.com");



routing.registerRoute(({ event, request, url }) => ["/"].includes(url.pathname), new strategies.StaleWhileRevalidate({ cacheName: "html" }));
routing.registerRoute(({ event, request, url }) => url.pathname.includes("app.webmanifest"), new strategies.StaleWhileRevalidate({ cacheName: "manifest" }));

routing.registerRoute(
    ({ event, request, url }) =>
        isGet(request)
        && request.destination === 'script'
        && !isPerlaResouce(url)
        && !isPerlaDependency(url),
    new strategies.NetworkFirst({ cacheName: 'scripts' })
);

routing.registerRoute(
    ({ event, request, url }) =>
        isGet(request)
        && request.destination === 'script'
        && !isPerlaResouce(url)
        && isPerlaDependency(url),
    new strategies.CacheFirst({ cacheName: 'cdn-cache' })
);


routing.registerRoute(
    ({ event, request, url }) => isGet(request) && request.destination === 'style',
    new strategies.StaleWhileRevalidate({ cacheName: 'styles' })
);
