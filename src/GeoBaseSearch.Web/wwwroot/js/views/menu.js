class Menu {
	constructor() {
		this.viewModel = new MenuViewModel();
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
				<select>
					${this.renderSelectOptions()}
				</select>
			</div>
`
		return result;
	}
}