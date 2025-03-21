defmodule Lms.Catalog do
  @moduledoc """
  The Catalog context. This context refers to material like Courses and Episodes;
  these are material 'consumed' by the end-user
  """

  import Ecto.Query, warn: false
  alias Lms.Repo

  alias Lms.Catalog.Course

  def list_courses do
    Repo.all(Course)
  end

  def get_course(id) do
    Repo.get(Course, id)
    |> case do
      course = %Course{} ->
        {:ok, course}
      _ ->
        {:error, "Not Found"}
    end
  end
end
