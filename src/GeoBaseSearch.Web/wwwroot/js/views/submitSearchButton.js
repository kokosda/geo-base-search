class SubmitSearchButton {
	constructor(options) {
		this.options = options;
		this.text = "Search";
	}

	setLoading() {
		this.text = "Loading...";
		document.getElementById("SubmitSearchButton").innerText = this.text;
	}

	setDefault() {
		this.text = "Search";
		document.getElementById("SubmitSearchButton").innerText = this.text;
	}

	render() {
		var result =
`
			<button type="button" id="SubmitSearchButton" onclick="${this.options.onClick}">${this.text}</button>
`
		return result;
	}
}