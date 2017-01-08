<FuzzySystem>
  <Element Name="Відстань" X="485" Y="501" Type="Variable">
    <Term Name="1-Близько" Type="Sigmoid" A="0" B="10" C="20" D="30" />
    <Term Name="2-Середньо" Type="Sigmoid" A="0" B="10" C="20" D="30" />
    <Term Name="3-Далеко" Type="Trapezoid" A="0" B="10" C="20" D="30" />
  </Element>
  <Element Name="Напрям" X="488" Y="599" Type="Variable">
    <Term Name="1-Зліва" Type="Trapezoid" A="0" B="10" C="20" D="30" />
    <Term Name="2-Зправа" Type="Trapezoid" A="0" B="10" C="20" D="30" />
  </Element>
  <Element Name="2" X="606" Y="551" Type="RuleBase">
    <Inference Type="Max" GammaParameter="1" />
    <Rule Weight="1">
      <Conditions>
        <Equal Var="Відстань" Val="1-Близько" />
        <Equal Var="Напрям" Val="1-Зліва" />
      </Conditions>
      <Results>
        <Equal Var="Поворот" Val="1-Різко вліво" />
      </Results>
    </Rule>
    <Rule Weight="1">
      <Conditions>
        <Equal Var="Відстань" Val="2-Середньо" />
        <Equal Var="Напрям" Val="1-Зліва" />
      </Conditions>
      <Results>
        <Equal Var="Поворот" Val="2-Вліво" />
      </Results>
    </Rule>
    <Rule Weight="1">
      <Conditions>
        <Equal Var="Відстань" Val="2-Середньо" />
        <Equal Var="Напрям" Val="2-Зправа" />
      </Conditions>
      <Results>
        <Equal Var="Поворот" Val="3-Вправо" />
      </Results>
    </Rule>
    <Rule Weight="1">
      <Conditions>
        <Equal Var="Відстань" Val="3-Далеко" />
        <Equal Var="Напрям" Val="2-Зправа" />
      </Conditions>
      <Results>
        <Equal Var="Поворот" Val="4-Різко вправо" />
      </Results>
    </Rule>
  </Element>
  <Element Name="Поворот" X="730" Y="552" Type="Variable">
    <Term Name="1-Різко вліво" Type="Trapezoid" A="0" B="10" C="20" D="30" />
    <Term Name="2-Вліво" Type="Trapezoid" A="0" B="10" C="20" D="30" />
    <Term Name="3-Вправо" Type="Trapezoid" A="0" B="10" C="20" D="30" />
    <Term Name="4-Різко вправо" Type="Trapezoid" A="0" B="10" C="20" D="30" />
  </Element>
  <Link Source="Відстань" Destination="2" />
  <Link Source="Напрям" Destination="2" />
  <Link Source="2" Destination="Поворот" />
</FuzzySystem>