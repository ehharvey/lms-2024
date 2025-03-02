defmodule Web.Repo do
  use Ecto.Repo,
    otp_app: :web,
    adapter: Ecto.Adapters.SQLite3
end
