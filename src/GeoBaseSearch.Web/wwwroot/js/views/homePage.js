class HomePage {
	constructor() {
		this.menu = new Menu();
	}

	render() {
		var result =
`
			${this.menu.render()}
`
		return result;
	}
}