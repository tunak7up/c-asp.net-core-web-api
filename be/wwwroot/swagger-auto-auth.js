(function () {
  const originalFetch = window.fetch;
  window.fetch = async function (...args) {
    const res = await originalFetch.apply(this, args);
    try {
      const url = typeof args[0] === "string" ? args[0] : args[0].url;
      if (url.toLowerCase().includes("/api/account/login") && res.ok) {
        const data = await res.clone().json();
        if (data.token && window.ui) {
          window.ui.preauthorizeApiKey("Bearer", data.token);
        }
      }
    } catch (e) {}
    return res;
  };
})();
