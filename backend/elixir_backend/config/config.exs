# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

config :elixir_backend,
  ecto_repos: [ElixirBackend.Repo]

# Configures the endpoint
config :elixir_backend, ElixirBackendWeb.Endpoint,
  url: [host: "localhost"],
  secret_key_base: "ZdNFU9ws+eFv6hQpFNt/+us1bnmp+R9pf+ek++6XxUXzzWMaiV+CtEx9XYQSezEe",
  render_errors: [view: ElixirBackendWeb.ErrorView, accepts: ~w(html json), layout: false],
  pubsub_server: ElixirBackend.PubSub,
  live_view: [signing_salt: "I3DFiKLj"]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
