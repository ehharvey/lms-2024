This page describes notes on modifying [[quartz.config.ts]]
# Overview
* The `.github/workflows/build-deploy-wiki.yml` GitHub Action builds our wiki
* This action clones the `quartz` wiki and runs build scripts
* In order to complete #36, we have a configuration file at `wiki/quartz.config.ts` that the GitHub Action copies and uses
# How-to's
## Changing header image
* By default, quartz will create a "Title" image using the `config.configuration.pageTitle` property
# References
* https://preemchro.me/Datadump/site-modifications#home-image
* https://quartz.jzhao.xyz/configuration