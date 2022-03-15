class ServiceRegistry {
	constructor(serviceRegistrySettings) {
		if (!serviceRegistrySettings)
			throw new Error("ServiceRegistrySettings is not defined.");

		this.serviceRegistrySettings = serviceRegistrySettings;
		this.container = new Map();
		this.container.set("locationApiService", new LocationApiService(this.serviceRegistrySettings.BaseApiUrl))
	}

	getService(name) {
		if (!this.container.has(name))
			throw new Error(`Service with name ${name} is not registered.`);

		var result = this.container.get(name);
		return result;
	}
}

