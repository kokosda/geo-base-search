class HomePage {
	constructor() {
		const menuSelectorChangeCallback = (menuState, e) => this.onMenuModeSelectorChangeCallback(menuState, e);
		this.menu = new Menu(menuSelectorChangeCallback);
		this.locationComponentFactory = {
			"LocationByIp": () => new LocationByIp(),
			"LocationsByCity": () => new LocationsByCity()
		};
	}

	onMenuModeSelectorChangeCallback(menuState, e) {
		this.renderLocationPage(menuState);
	}

	renderLocationPage(menuState) {
		if (!this.locationComponentFactory[menuState.selectorState])
			throw new Error(`Unsupported component type with name ${menuState.selectorState}`);

		const component = this.locationComponentFactory[menuState.selectorState]();
		const locationPageElement = document.getElementById("LocationPage");
		const result = component.render();

		if (locationPageElement)
			locationPageElement.innerHTML = result;

		return result;
	}

	render() {
		var result =
`
			${this.menu.render()}
			<div id="LocationPage">
				${this.renderLocationPage(this.menu.getState())}
			</div>
`
		return result;
	}
}