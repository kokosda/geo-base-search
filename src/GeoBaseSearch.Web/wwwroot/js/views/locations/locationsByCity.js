class LocationsByCity extends LocationComponentBase {
	constructor() {
		super("LocationsByCity");
		this.searchInput = new SearchInput({ placeholder: "ex. Vancouver" });
		window.GeoBaseSearchState.LocationsByCity = this;
	}

	getDataLoadingFunction() {
		var result = (city) => this.locationApiService.getLocationsByCity(city);
		return result;
	}

	render() {
		var result =
			`
		<div id="LocationsByCity">
			<div id="Controls">
				<span>Enter city to search corresponding locations:</span>
				${this.searchInput.render()}
				${this.submitSearchButton.render()}
			</div>
			<div id="ResultView" />
		</div>
`
		return result;
	}
}