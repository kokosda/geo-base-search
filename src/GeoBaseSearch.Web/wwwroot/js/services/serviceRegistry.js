class ServiceRegistry {
	constructor(serviceRegistrySettings) {
		if (!serviceRegistrySettings)
			throw new Error("ServiceRegistrySettings is not defined.");

		this.serviceRegistrySettings = serviceRegistrySettings;
		this.container = new Map();
		this.container.set("locationApiService", new LocationApiService(this.serviceRegistrySettings.BaseApiUrl))
	}
}

