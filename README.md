# MarsRover

This app is a RESTful api that currently has a single endpoint "/api/image'

It expects a file called "dates.txt" to be located in C:\MarsRover containing a list of dates to query.
It queries the NASA api and downloads any images from the selected dates that are not already downloaded.
Finally, it returns a list of all images as they were received from NASA, but containing only the id and source url.
