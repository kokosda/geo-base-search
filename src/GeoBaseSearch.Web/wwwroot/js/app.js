class App {
	constructor(appSettingsString) {
		if (!appSettingsString)
			throw new Error("App settings string is not defined.")

		this.appSettings = JSON.parse(appSettingsString);
		this.appSettingsString = appSettingsString;

		window.GeoBaseSearchState = {
			serviceRegistry: new ServiceRegistry({ BaseApiUrl: this.appSettings.BaseApiUrl })
		};
	}

	run() {
		console.log(`Running the app with the following settings: ${this.appSettingsString}`);

		if (!HomePage)
			throw new Error("HomePage object is not defined.")

		const appElement = document.getElementById("app");

		if (!appElement)
			throw new Error("App element is not defined.")

		const homePage = new HomePage();
		const markup = homePage.render();
		appElement.innerHTML = markup;
	}
}