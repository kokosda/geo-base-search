class MenuViewModel {
	constructor() {
		this.selectorState = 0;
		this.selectorStates = [
			{ value: "LocationByIp", text: "Geo Information by IP", view: new LocationByIp() },
			{ value: "LocationsByCity", text: "Locations by City", view: new LocationByIp() }
		]
	}
}