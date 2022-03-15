class SubmitSearchButton {
	constructor(options) {
		this.options = options;
		this.text = "Search";
	}

	makeLoading() {
		this.text = "Loading...";
	}

	makeDefault() {
		this.text = "Search";
	}

	render() {
		var result =
`
			<button type="button" id="SubmitSearchButton" onclick="${this.options.onClick}">${this.text}</button>
`
		return result;
	}
}