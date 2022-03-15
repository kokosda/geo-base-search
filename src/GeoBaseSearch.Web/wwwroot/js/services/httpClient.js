class HttpClient {
	get(url) {
		if (!url)
			throw new Error("URL is not defined.")

		return fetch(url).then(data => data.json()).then(res => res);
	}
}