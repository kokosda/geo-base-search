class MenuViewModel {
	constructor() {
		this.selectorState = "LocationByIp";
		this.selectorStates = [
			{ value: "LocationByIp", text: "Geo Information by IP" },
			{ value: "LocationsByCity", text: "Locations by City" }
		]
	}
}