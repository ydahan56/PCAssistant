# PCAssistant

PCAssistant (formerly Telebot) is a Telegram bot that allows you to do many cool things with your PC remotely controlled from Telegram. This project was born somewhere around 2018, when I was contacted by a discharged military soldier who, at the time, worked as a freelance PC technician as a passive income. At the time, he needed a tool that allowed him to monitor his customer's computers remotely in order to easily troubleshoot problems and fix them. As our partnership progressed, we kept adding more features according to his needs in order to make it more personal and customized. In 2023, the project has been rebranded as PCAssistant and has been completely rewritten almost from scratch for performance and [flexibility](https://en.wikipedia.org/wiki/Plug-in_(computing)). The project has been opened source with permission and will allow other developers to fork their own variations in order to allow custom builds for the public. My goal with the project is to help power users to leverage the benefits it will give them.

# Some of the features

- Monitoring of hardware with cron-like syntax
- Adjust workstation's brightness (supported machines only)
- Change the workstation's state, sleep, lock and so on..
- Obtain full system report about the workstation

# FAQ

### Q: What to do if the driver is not loading?
![DriverError](https://raw.githubusercontent.com/ydahan56/PCAssistant/refs/heads/main/2024-09-19%2021_16_13-Program%20Compatibility%20Assistant.png)

### A: Disable [memory integrity](https://support.microsoft.com/en-us/windows/a-driver-can-t-load-on-this-device-8eea34e5-ff4b-16ec-870d-61a4a43b3dd5) in your AV. PCAssistant relies on this driver for core operations on hardware such as temperature, utilization and so on..
