defmodule Lms.Repo.Migrations.CreateCourses do
  use Ecto.Migration

  def change do
    create table(:courses) do
      add :title, :string
      add :tagline, :string
      add :description, {:array, :string}

      timestamps(type: :utc_datetime)
    end
  end
end
