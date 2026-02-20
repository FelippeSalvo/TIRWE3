const TOKEN_KEY = 'fitness_token';

function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

function setToken(token) {
  if (token) localStorage.setItem(TOKEN_KEY, token);
  else localStorage.removeItem(TOKEN_KEY);
}

function getBaseUrl() {
  return window.location.origin;
}

async function apiGet(path) {
  const token = getToken();
  const opts = { headers: {} };
  if (token) opts.headers['Authorization'] = 'Bearer ' + token;
  const res = await fetch(getBaseUrl() + path, opts);
  if (res.status === 401) {
    setToken(null);
    window.location.href = 'index.html';
    throw new Error('Não autorizado');
  }
  const text = await res.text();
  if (!res.ok) throw new Error(text || res.statusText);
  return text ? JSON.parse(text) : null;
}

async function apiPost(path, body) {
  const token = getToken();
  const opts = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  };
  if (token) opts.headers['Authorization'] = 'Bearer ' + token;
  const res = await fetch(getBaseUrl() + path, opts);
  if (res.status === 401) {
    setToken(null);
    window.location.href = 'index.html';
    throw new Error('Não autorizado');
  }
  const text = await res.text();
  if (!res.ok) throw new Error(text || res.statusText);
  return text ? JSON.parse(text) : null;
}

async function apiPut(path, body) {
  const token = getToken();
  const opts = {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  };
  if (token) opts.headers['Authorization'] = 'Bearer ' + token;
  const res = await fetch(getBaseUrl() + path, opts);
  const text = await res.text();
  if (!res.ok) throw new Error(text || res.statusText);
  return text ? JSON.parse(text) : null;
}

function checkAuth() {
  if (!getToken()) {
    window.location.href = 'index.html';
    return false;
  }
  return true;
}
