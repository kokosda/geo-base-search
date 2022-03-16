class LocationComponentBase {
	constructor(locationComponentName) {
		if (typeof locationComponentName !== "string")
			throw new Error("Component name must be specified.")

		this.locationApiService = window.GeoBaseSearchState.serviceRegistry.getService("locationApiService");
		this.submitSearchButton = new SubmitSearchButton({ onClick: `window.GeoBaseSearchState.${locationComponentName}.onSubmitSearchButtonClick()` });

		window.GeoBaseSearchState[locationComponentName] = this;
	}

	async getLocationData(dataLoadingFunction) {
		const searchInputValue = this.searchInput.getValue();
		this.submitSearchButton.setLoading();
		let result = "";

		try {
			result = await dataLoadingFunction(searchInputValue);
		} catch (e) {
			console.error(e);
			result = e.message;
		} finally {
			this.submitSearchButton.setDefault()
		}

		return result;
	}

	async onSubmitSearchButtonClick() {
		const dataLoadingFunction = this.getDataLoadingFunction();
		const data = await this.getLocationData(dataLoadingFunction);
		const resultViewElement = document.getElementById("ResultView");
		resultViewElement.innerText = data;
	}
}