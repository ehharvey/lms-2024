defmodule Lms.Repo do
  use Ecto.Repo,
    otp_app: :lms,
    adapter: Ecto.Adapters.SQLite3
end
