class HttpClient {
	async get(url) {
		if (!url)
			throw new Error("URL is not defined.")

		return await fetch(url).then(data => data.json()).then(res => res).catch(err => console.error(err));
	}
}