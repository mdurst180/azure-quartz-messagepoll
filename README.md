# Quartz.NET jobs scheduled within Azure Worker Role Example

This project provides an example of how to use a Azure Worker Role to schedule secondly, daily & weekly jobs using  Quartz.NET.

# Overview


**QuartzManager** manages the Quartz scheduler and starts the jobs.

Jobs are configured in app.config with a custom configsection block.

The readers for the configsection are defined in the **Configuration** folder.


# Learn More

For documentation on Quartz.NET, please see [Quartz.NET Home](http://quartznet.sourceforge.net/).