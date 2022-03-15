class Menu {
	constructor() {
		this.viewModel = new MenuViewModel();

		if (!window.GeoBaseSearchState.Menu)
			window.GeoBaseSearchState.Menu = this;
	}

	onModeSelectorChange() {
		console.log("clicked");
	}

	renderSelectOptions() {
		let result = "";

		for (let i = 0; i < this.viewModel.selectorStates.length; i++) {
			const selectorState = this.viewModel.selectorStates[i];
			result +=
`
			<option value="${selectorState.value}">${selectorState.text}</option>
`
		}

		return result;
	}

	render() {
		var result =
`
			<div class="menu">
				<span>Select view</span>
				<select id="ModeSelector" onchange="window.GeoBaseSearchState.Menu.onModeSelectorChange()">
					${this.renderSelectOptions()}
				</select>
			</div>
			${this.viewModel.selectorStates[this.viewModel.selectorState].view.render()}
`
		return result;
	}
}