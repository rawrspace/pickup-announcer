# Pickup Announcer

- Announce student pickups to all Teachers who are connected to the SignalR hub.
- Define Grade Levels and customize the announcement colors.
- Configure the number of cones available for pickup.

## Setup

The MSSQL Database Project needs to be published before the application can run. Each instance of the application will require a separate database instance.

## Running with Docker

You can run the application by using the [official Docker image](https://hub.docker.com/r/vandersmissenc/pickup-announcer)

Make sure you mount the appropriate appsettings.json file with the ConnectionString set for DefaultConnection so that the container can access your MSSQL instance.

## Running without Docker

After cloning the solution open in Visual Studio/VS Code and package the application for deployment based on your target environment.

Make sure to modify the appsettings.json file with the ConnectionString set for DefaultConnection so that the container can access your MSSQL instance.

## Configuration

Once the database has been deployed you should update the Config.Site table to set new values for AdminUser and AdminPass. Be sure to communicate these values to the Principal of the school who will use the application.

*Note: This admin account is only meant to prevent accidental access to the admin panel. Therefore the password is stored in plain text. Please do NOT reuse a password that could be comprimised.*

## Usage

### Admin - Add new registrations

To get started the administrator needs to login to the admin page using the credentials defined in the database. Once logged in they can download a copy of the database which will give them the template to use for adding new student registrations. After entering the new records the file can be uploaded into the data. This is a flush and fill operation and anything removed from the file will also be gone from the database.

### Admin - Add/Modify grade levels
The administrator can add new grade levels and alter the color for the notification background and foreground. After making changes the save icon on the far right of the row must be used to commit changes.

### Admin - Adjust number of codes
The administrator can change the number of cones that are available on the announcer page. Whatever number is set will generate that many buttons to be used by the announcer.

### Teachers - Watch for announcements

Teachers will use the application by navigating to the main page and simply leaving the app open. On initial connect all of the announcements for the day will automatically download and then an open SignalR connection will be used to send additional announcements.

### Car Rider Announcer - Announce new pickups

The announcer working car rider pickup will use the Announcement page to type in the registration number and the cone where the car will be location. Once submitted the registration ID will be checked and if valid an announcement will be sent.

## Contributing
Check out [CONTRIBUTING.md](CONTRIBUTING.md) for more info.

## License
[![License](https://img.shields.io/github/license/rawrspace/pickup-announcer.svg)](https://github.com/rawrspace/pickup-announcer/blob/master/LICENSE)