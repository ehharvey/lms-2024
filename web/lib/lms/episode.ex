defmodule Lms.Episode do
  use Ecto.Schema
  import Ecto.Changeset

  schema "episodes" do
    field :title, :string

    timestamps(type: :utc_datetime)
  end

  @doc false
  def changeset(episode, attrs) do
    episode
    |> cast(attrs, [:title])
    |> validate_required([:title])
  end
end
