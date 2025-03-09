defmodule Lms.Repo.Migrations.CreateEpisodes do
  use Ecto.Migration

  def change do
    create table(:episodes) do
      add :title, :string

      timestamps(type: :utc_datetime)
    end
  end
end
