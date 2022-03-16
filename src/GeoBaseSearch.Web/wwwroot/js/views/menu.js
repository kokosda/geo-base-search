class Menu {
	constructor(onModelSelectorChangeCallback) {
		const state = {
			selectorState: "LocationByIp",
			selectorStates: [
				{ value: "LocationByIp", optionText: "Geo Information by IP" },
				{ value: "LocationsByCity", optionText: "Locations by City" }
			]
		};

		this.setState(state);
		this.onModeSelectorChangeCallBack = onModelSelectorChangeCallback;
		window.GeoBaseSearchState.Menu = this;
	}

	getState() {
		const result = Object.create(window.GeoBaseSearchState.MenuState);
		return result;
	}

	setState(state) {
		window.GeoBaseSearchState.MenuState = Object.create(state);
	}

	onModeSelectorChange(e) {
		console.log("clicked");
		const state = this.getState();
		state.selectorState = document.getElementById("ModeSelector").value;
		this.setState(state);
		this.onModeSelectorChangeCallBack(state, e);
	}

	renderSelectOptions() {
		const state = this.getState();
		let result = "";

		for (let i = 0; i < state.selectorStates.length; i++) {
			const selectorState = state.selectorStates[i];
			result +=
`
			<option value="${selectorState.value}">${selectorState.optionText}</option>
`
		}

		return result;
	}

	render() {
		var result =
`
			<div class="menu">
				<span>Select view</span>
				<select id="ModeSelector" onchange="window.GeoBaseSearchState.Menu.onModeSelectorChange(event)">
					${this.renderSelectOptions()}
				</select>
			</div>
`
		return result;
	}
}