const API_URL = "http://localhost:5000/api"; // Ajusta el puerto según tu launchSettings.json

async function apiFetch(endpoint, options = {}) {
    const token = localStorage.getItem("jwt_token");
    const headers = {
        "Content-Type": "application/json",
        ...(token ? { "Authorization": `Bearer ${token}` } : {}),
        ...options.headers
    };

    const response = await fetch(`${API_URL}${endpoint}`, { ...options, headers });

    if (response.status === 401) {
        localStorage.removeItem("jwt_token");
        window.location.href = "index.html";
        return;
    }

    if (!response.ok) {
        const err = await response.json().catch(() => ({ message: "Error en la solicitud" }));
        throw new Error(err.message || "Error inesperado");
    }

    return response.status === 204 ? null : response.json();
}
