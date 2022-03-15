class SearchInput {
	constructor(options) {
		this.options = options;
	}

	getValue() {
		const result = document.getElementById("SearchInput").value;
		return result;
	}

	render() {
		var result =
`
		<input type="text" minlength="1" id="SearchInput" placeholder="${this.options.placeholder}" />
`
		return result;
	}
}