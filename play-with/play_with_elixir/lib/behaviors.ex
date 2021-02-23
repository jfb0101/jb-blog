defmodule Example.Worker do
  @callback init(state :: term) :: {:ok, new_state :: term} | {:error, reason :: term}
  @callback perform(args :: term, state :: term) ::
              {:ok, result :: term, new_state :: term}
              | {:error, reason :: term, new_state :: term}
end


defmodule Example.Downloader do
  @behaviour Example.Worker

  def init(opts), do: {:ok,opts}

  def perform(_url,_opts) do
    {:ok,"result","new_state"}
  end
end
