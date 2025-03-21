defmodule Lms.Catalog.Course do
  use Ecto.Schema
  import Ecto.Changeset

  schema "courses" do
    field :title, :string
    field :tagline, :string
    field :description, {:array, :string}


    timestamps(type: :utc_datetime)
  end

  @doc false
  def changeset(course, attrs) do
    course
    |> cast(attrs, [:title, :tagline, :description])
    |> validate_required([:title, :tagline, :description])
  end
end
