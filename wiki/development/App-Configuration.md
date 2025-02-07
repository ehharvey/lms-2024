This page describes design for app configuration.

# Overview
App configuration let us change the behaviour of our program without modifying code. Maybe we have a big feature that we want to implement. It's not ideal to add it straight to a branch. So instead, we could "gate" it behind a feature flag (a specific type of app config). We then can continuously add code to incorporate that feature. Then, when we're ready, we can flip the switch on a feature flag (e.g,. by changing the default value).

# Design overview
Configuration is delivered via files read from a directory (default `./config/`). Each file name refers to a property name, and each file's contents refers to the value of the property.

The directory used to read configuration from can be altered the environment variable `LMS_CONFIG_DIRECTORY`.

# Consuming App Config
Consume config via `Global.config`. `Program.cs` has an example used to toggle displaying the config at app startup.

Developers should make a copy of `config.sample` directory as `config`.

# Adding App Config
**Try to add all possible config as you develop**. This includes DB credentials, feature flags, and alternate implementations.

Do so by adding properties to `Lms._Config`. Required parameters should not set a default value. After adding a property, you should configure a developer-reasonable value as a file in `config.sample`.