## Task:  

Develop a web application to get the coordinates of a user by their IP address and get a list of locations by city name.

### Development tools:  

1.  C#, ASP.NET Core  
2.  MS Visual Studio  
3.  HTML5, CSS3, JavaScript
4.  Without using a DBMS  
    

### Requirement for architecture and source code:

1.  The web application must be designed and developed for 10,000,000 unique users per day and 100,000,000 requests per day.
2.  The client part should be implemented as a Single Page Application, be as lightweight as possible, written in JavaScript without using third-party frameworks for SPA.
3.  The source code must be formatted in the same style and contain the necessary comments.
4.  The neatness of the source code will be judged along with the functionality of the application.
5.  There are no minimum supported browser version requirements for client code. You can use the features of the latest versions of browsers (Chrome, Firefox, Edge).
6.  Platform: Windows 10 or Windows Server 2019.
7.  The submitted test task must not contain executable *.exe or *.dll files.

### Application technical description:

-   The database is stored in the [geobase.dat](https://github.com/kokosda/geo-base-search/blob/main/artifacts/test-dot-net-geobase.zip) file , which is contained in the archive attached to the letter. The database will not change, it is read-only.
-   The database is in binary format. The file is sequentially stored:  
    
    60 bytes - header
    
		int version ; // database version 
		sbyte name[32]; // name/prefix for the database 
		ulong timestamp; // database creation time 
		int records; // total number of entries 
		uint offset_ranges; // offset from the beginning of the file to the beginning of the list of records with geoinformation 
		uint offset_cities; // offset from the beginning of the file to the beginning of the index, sorted by city names 
		uint offset_locations; // offset from the beginning of the file to the beginning of the list of location records
    
    12 bytes * Header.records (number of records) - a list of records with information about IP address intervals, sorted by the ip_from and ip_to fields
    
	    uint ip_from; // start of IP address range 
	    uint ip_to; // end of IP address range 
	    uint location_index; // index of location record
    
    96 bytes * Header.records (number of records) - a list of records with location information with coordinates (longitude and latitude)
    
	    sbyte country[8]; // country name (random string with "cou_" prefix) 
	    sbyte region[12]; // region name (random string with "reg_" prefix) 
	    sbyte postal[12]; // postal code (random string with "pos_" prefix) 
	    sbyte city[24]; // city name (random string with "cit_" prefix) 
	    sbyte organization[32]; // organization name (random string with "org_" prefix) 
	    float latitude; // latitude 
	    float longitude; // longitude
    
    4 bytes * Header.records (number of records) - list of location record indexes sorted by city name, each index is the address of the record in the file relative to Header.offset_locations
    
-   The database is loaded completely into memory when the application starts.
-   The database must be loaded into memory in one thread, without parallelization.  
    
-   The time of loading the database into memory should be no more than 30 ms (reading from SSD).  
    
-   It is necessary to implement a quick (binary) search in the loaded database by IP address and by the exact match of the city name, case sensitive.

-   The application must implement two HTTP API methods:  
    
	    -   GET /ip/location?ip=123.234.123.234
	    -   GET /city/locations?city=cit_Gbqw4  
        
    The server response to each of the requests must be in JSON format.  
    
-   The client part of the application must be implemented in the Single Page Application ideology.  
    
-   The page should consist of two parts: on the left side of the screen switching menu, on the right side the selected screen is displayed.  
    
-   The client part must implement two screens: search for geo-information by IP, search for a list of locations by city name.  
    
-   The geo-information search screen contains: a field for entering an IP address, a "Search" button and an area for displaying the result.  
    By clicking the "Search" button, a GET /ip/location?ip=123.234.123.234 request is sent to the server.  
    The processed response from the server is displayed in the results output area.  
    
-   The location list search screen contains: a field for entering the name of the city, a "Search" button, and an area for displaying the result.  
    By clicking the "Search" button, a GET /city/locations?city=cit_Gbqw4 request is sent to the server.  
    The processed response from the server is displayed in the results output area.  
    

### Additionally:

-   The manifestation of initiative beyond the main task is welcome.
-   Any code comments are welcome

### Investments:

-   [database geobase.dat](https://github.com/kokosda/geo-base-search/blob/main/artifacts/test-dot-net-geobase.zip)

### How to Build:

1. MS Visual Studio 2022 / Rider
2. GeoBaseSearch.Api: build and run an API server first
3. GeoBaseSearch.Web: build an run web server.
4. Access a web page in browser.
