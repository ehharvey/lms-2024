This page describes testing.

# Overview
1. Our work should be tackled by unit, integration, and system testing
2. Unit and integration testing can be performed within the `app-test` dotnet project
3. System testing is TBD

## Unit/Integration testing
These are "function-style" tests. See `./app-test/...` for our current tests.

We measure the "code coverage" of these tests--how much of the code the tests covers.

Our GitHub actions will report test coverage. However, you can do so locally in VS Code.
1. Ensure you have the appropriate extensions installed (see [[Getting Started]])
2. Go to the VS Code test pane
3. On the top-right, run 'Run Tests with Coverage'