# This file is responsible for configuring your application
# and its dependencies with the aid of the Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
import Config

config :lms,
  ecto_repos: [Lms.Repo],
  generators: [timestamp_type: :utc_datetime]

# Configures the endpoint
config :lms, LmsWeb.Endpoint,
  url: [host: "localhost"],
  adapter: Bandit.PhoenixAdapter,
  render_errors: [
    formats: [html: LmsWeb.ErrorHTML, json: LmsWeb.ErrorJSON],
    layout: false
  ],
  pubsub_server: Lms.PubSub

# Configures the mailer
#
# By default it uses the "Local" adapter which stores the emails
# locally. You can see the emails in your browser, at "/dev/mailbox".
#
# For production it's recommended to configure a different adapter
# at the `config/runtime.exs`.
config :lms, Lms.Mailer, adapter: Swoosh.Adapters.Local

# Configure esbuild (the version is required)
config :esbuild,
  version: "0.17.11",
  lms: [
    args:
      ~w(--bundle --target=es2017 --outdir=../priv/static/assets --external:/fonts/* --external:/images/*),
    cd: Path.expand("../assets", __DIR__),
    env: %{"NODE_PATH" => Path.expand("../deps", __DIR__)}
  ]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{config_env()}.exs"

config :kaffy,
  # required keys
  otp_app: :lms, # required
  ecto_repo: Lms.Repo, # required
  router: LmsWeb.Router, # required
  # optional keys
  admin_title: "LMS Admin",
  # admin_logo: [
  #   url: "https://example.com/img/logo.png",
  #   style: "width:200px;height:66px;"
  # ],
  # admin_logo_mini: "/images/logo-mini.png",
  enable_context_dashboards: true, # since v0.10.0
  admin_footer: "Kaffy &copy; 2023" # since v0.10.0
