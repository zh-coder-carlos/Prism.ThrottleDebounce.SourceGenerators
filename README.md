# Prism.ThrottleDebounce.SourceGenerators

一个面向 Prism（WPF）MVVM 项目的源生成器（Source Generator），通过简单的特性标注，为方法自动生成带 **节流（Throttle）** 或 **防抖（Debounce）** 行为的 `DelegateCommand`，无需手写样板代码。

## 简介

在 UI 开发中，常见需求是限制命令的触发频率：

- **节流（Throttle）**：在指定时间窗口内只执行一次，之后保持固定频率执行；
- **防抖（Debounce）**：只有在一段时间内没有再次触发时，才执行最后一次调用（常用于搜索框输入、窗口缩放等场景）。

本库通过在方法上添加 `[ThrottleCommand]` 或 `[DebounceCommand]` 特性，编译期自动生成对应的 `DelegateCommand` 属性，底层复用 [ThrottleDebounce](https://www.nuget.org/packages/ThrottleDebounce) 库的能力。

## 安装

通过 NuGet 安装：

```bash
dotnet add package Prism.ThrottleDebounce.SourceGenerators
```

> 使用本生成器的项目需要同时引用以下依赖（生成代码会在编译期展开到你的项目中）：
>
> - **Prism**（提供 `DelegateCommand`）
> - **ThrottleDebounce**（提供 `Throttler` / `Debouncer`）

## 快速开始

1. 将 ViewModel 声明为 `partial class`（源生成器需要向其中注入成员）。
2. 在方法上添加 `[ThrottleCommand]` 或 `[DebounceCommand]` 特性。
3. 在 XAML 中绑定自动生成的 `{方法名}Command` 属性。

```csharp
using Prism.Mvvm;
using Prism.ThrottleDebounce.SourceGenerators;

public partial class SearchViewModel : BindableBase
{
    // 节流：300ms 内只触发一次
    [ThrottleCommand(300)]
    private void OnRefresh()
    {
        // 刷新逻辑
    }

    // 防抖：停止输入 500ms 后才执行
    [DebounceCommand(500)]
    private void OnSearch(string keyword)
    {
        // 搜索逻辑
    }
}
```

```xml
<!-- 无参数命令 -->
<Button Content="刷新" Command="{Binding OnRefreshCommand}" />

<!-- 带参数命令（绑定 TextBox 的文本） -->
<Button Content="搜索" Command="{Binding OnSearchCommand}" CommandParameter="{Binding Text, ElementName=SearchBox}" />
```

## 特性详解

### `[ThrottleCommand(int delayMs)]`

为方法生成一个节流命令。默认参数：

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| `delayMs` | `int` | 必填 | 节流时间窗口（毫秒） |
| `Leading` | `bool` | `true` | 窗口开始时是否立即执行 |
| `Trailing` | `bool` | `true` | 窗口结束时是否补执行 |

### `[DebounceCommand(int delayMs)]`

为方法生成一个防抖命令。默认参数：

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| `delayMs` | `int` | 必填 | 防抖等待时间（毫秒） |
| `Leading` | `bool` | `false` | 首次触发时是否立即执行 |
| `Trailing` | `bool` | `true` | 最后一次触发后是否执行 |

自定义 `Leading` / `Trailing` 示例：

```csharp
[ThrottleCommand(300, Leading = false)]
private void OnLeadingDisabled() { }

[DebounceCommand(500, Leading = true)]
private void OnLeadingEnabled(string text) { }
```

## 生成的命令

生成器根据方法名生成同名（+`Command` 后缀）的命令属性：

| 方法 | 生成的命令 | 命令类型 |
| --- | --- | --- |
| `void OnRefresh()` | `OnRefreshCommand` | `DelegateCommand` |
| `void OnSearch(string keyword)` | `OnSearchCommand` | `DelegateCommand<string>` |

- 无参数方法 → 生成非泛型 `DelegateCommand`；
- 带参数方法 → 按参数类型生成对应的泛型 `DelegateCommand<T1, T2, ...>`；
- 生成代码位于目标类的同一命名空间下的 `partial class` 中，方法本身保持 `private` 即可。

## 前置要求

- **面向 .NET 9 及以上**，或显式设置 `<LangVersion>preview</LangVersion>`。

  > 生成代码使用了 C# 13 的 `field` 关键字（field-backed properties），需要相应语言版本支持。

- 目标类必须声明为 `partial`。
- 项目需引用 Prism 与 ThrottleDebounce 两个 NuGet 包。

## 本地构建与打包

```bash
dotnet build
```

项目已启用 `GeneratePackageOnBuild`，构建成功后会在 `nupkg/` 目录下生成 NuGet 包（默认版本见 `.csproj` 中的 `Version`）。

## License

[MIT](./LICENSE)
