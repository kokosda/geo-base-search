class LocationByIp {
	constructor() {
		if (!window.GeoBaseSearchState.LocationByIp)
			window.GeoBaseSearchState.LocationByIp = this;

		this.locationApiService = window.GeoBaseSearchState.serviceRegistry.getService("locationApiService");
		this.searchInput = new SearchInput({ placeholder: "ex. 71.23.45.11" });
		this.submitSearchButton = new SubmitSearchButton({ onClick: "window.GeoBaseSearchState.LocationByIp.onSubmitSearchButtonClick()" });
	}

	getLocationByIpData() {
		const searchInputValue = this.searchInput.getValue();
		this.submitSearchButton.makeLoading();
		const resultViewElement = document.getElementById("ResultView");

		try {
			this.locationApiService.getLocationByIp(searchInputValue)
				.then(data => resultViewElement.innerText = data)
				.catch(err => resultViewElement.innerText = err)
				.finally(() => this.submitSearchButton.makeDefault());
		} catch (e) {
			this.submitSearchButton.makeDefault();
			resultViewElement.innerText = e;
		}
	}

	onSubmitSearchButtonClick() {
		this.getLocationByIpData();
	}

	render() {
		var result =
`
		<div id="LocationsByIp">
			<div id="Controls">
				<span>Enter IP address to search corresponding location:</span>
				${this.searchInput.render()}
				${this.submitSearchButton.render()}
			</div>
			<div id="ResultView" />
		</div>
`
		return result;
	}
}