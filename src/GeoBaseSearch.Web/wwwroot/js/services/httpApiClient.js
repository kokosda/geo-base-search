class HttpApiClient {
	get(url) {
		if (!url)
			throw new Error("URL is not defined.")

		const result = fetch(url)
			.then(async response => {
				let responseResult = await response.text();

				if (!responseResult) {
					responseResult = `Response status: ${response.status}`;
					console.error(response);
				}

				return responseResult;
			});
		return result;
	}
}