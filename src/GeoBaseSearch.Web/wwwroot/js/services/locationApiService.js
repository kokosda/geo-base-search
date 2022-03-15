﻿class LocationApiService {
	constructor(baseUrl) {
		if (!baseUrl)
			throw new Error("BaseUrl is not defined.");

		this.baseUrl = baseUrl;
		this.httpClient = new HttpClient();
	}

	async getLocationByIp(ip) {
		if (!ip)
			throw new Error("IP parameter is not defined.");

		const locationByIpUrl = this.composeUrl(`/ip/location/?ip=${ip}`);
		const result = await this.httpClient.get(locationByIpUrl);
		return result;
	}

	async getLocationsByCity(city) {
		if (!city)
			throw new Error("City parameter is not defined.");

		const locationsByCityUrl = this.composeUrl(`/city/locations/?city=${city}`);
		const result = await this.httpClient.get(locationsByCityUrl);
		return result;
	}

	composeUrl(relativeUrl) {
		const result = `${this.baseUrl}${relativeUrl}`;
		return result;
	}
}