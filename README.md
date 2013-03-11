# Quartz.NET scheduled Azure Queue Message Poller

This project provides an example of how to use an Azure Worker Role to schedule a message polling job. The message poller peeks at an Azure Queue and processes a message represented as a JSON object if present.

# Overview


**QuartzManager** manages the Quartz scheduler and starts the jobs.

Jobs are configured in app.config with a custom configsection block.

The readers for the configsection are defined in the **Configuration** folder.

**IQueue** represents an Azure Queue

**Tests** include test cases using Moq to mock out the Queues.


# Learn More

For documentation on Quartz.NET, please see [Quartz.NET Home](http://quartznet.sourceforge.net/).