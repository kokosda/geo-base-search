class LocationByIp extends LocationComponentBase {
	constructor() {
		super("LocationByIp");
		this.name = "LocationByIp";
		this.searchInput = new SearchInput({ placeholder: "ex. 71.23.45.11" });
		window.GeoBaseSearchState.LocationByIp = this;
	}

	getDataLoadingFunction() {
		var result = (ip) => this.locationApiService.getLocationByIp(ip);
		return result;
	}

	render() {
		var result =
`
		<div id="${this.name}">
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