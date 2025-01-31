This page describes design for app configuration.

# Overview
App configuration let us change the behaviour of our program without modifying code. Maybe we have a big feature that we want to implement. It's not ideal to add it straight to a branch. So instead, we could "gate" it behind a feature flag (a specific type of app config). We then can continuously add code to incorporate that feature. Then, when we're ready, we can flip the switch on a feature flag (e.g,. by changing the default value).

# Design overview
Configuration is delivered via a JSON file (on-disk) only. It is read from `app/config.json` by default (though the path can be redefined by setting `LMS_CONFIG_PATH` env var).

We use the Newtonsoft Json library to parse JSON. It is read to a global singleton `_Config` object, which is accessible via the `Global` static class. It is read-only.

There is a provision in the config for app versioning, however this is ignored. Emil couldn't decide how best to implement this.

# Consuming App Config
Consume config via `Global.config`. `Program.cs` has an example used to toggle displaying the config at app startup.

Developers should make a copy of `config.json.example` as `config.json`.

# Adding App Config
**Try to add all possible config as you develop**. This includes DB credentials, feature flags, and alternate implementations.

Do so by adding properties to `Lms._Config`. Required parameters should not set a default value.