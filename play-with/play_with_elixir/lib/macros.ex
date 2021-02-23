defmodule Difference do
  defmacro m_test(x) do
    IO.puts("#{inspect(x)}")
    x
  end

  def f_test(x) do
    IO.puts("#{inspect(x)}")
    x
  end
end

defmodule Double do
  defmacro double(x) do
    quote do: 2 * unquote(x)
  end
end

defmodule Logic do
  defmacro unless(condition,options) do
    quote do
      if(!unquote(condition),unquote(options))
    end
  end
end

defmodule MyMacro do
  defmacro fooMacro x do
    quote do
      unquote(x)
    end
  end
end


defmodule FunctionMaker do


  defmacro __using__(_opts) do
    quote do
      import FunctionMaker
    end
  end

  
  defmacro create_multiplier(function_name,factor) do
    quote do
      def unquote(function_name)(value) do
        unquote(factor) * value
      end
    end
  end


  defmacro create_functions(planemo_list) do
    Enum.map planemo_list, fn {name,gravity} ->
      quote do
        def unquote(:"#{name}_drop")(distance) do
          :math.sqrt(2 * unquote(gravity) * distance)
        end
      end
    end
  end

end

defmodule Multiply do
  use FunctionMaker

  create_multiplier :double, 2
  # FunctionMaker.create_multiplier :triple, 3

  def example do
    x = double(12)
    IO.puts("12 x 2 is #{x}")
  end
end

defmodule Drop do
  require FunctionMaker

  FunctionMaker.create_functions([{:mercury,3.7},{:venus, 8.9}])
end
