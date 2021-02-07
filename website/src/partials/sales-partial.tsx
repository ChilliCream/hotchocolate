import { FunctionComponent, useState } from "react";
import styled from "styled-components";
import { SalesCard } from "../components/sales-card";

type Cycle = "monthly" | "biannualy" | "annualy";
type Plan = "Basic" | "Enterprize";

export const SalesPartial: FunctionComponent = () => {
  const [cycle, setCycle] = useState<Cycle>("annualy");

  const planPrices: Record<Plan, Record<Cycle, number>> = {
    Basic: {
      monthly: 325,
      biannualy: 300,
      annualy: 275,
    },
    Enterprize: {
      monthly: 2250,
      biannualy: 2000,
      annualy: 1750,
    },
  };

  return (
    <>
      <Container>
        <Title>Support Plans</Title>
        <LeadingText>
          Do you use HotChocolate or any other ChilliCream product? Do you need
          help? Choose one of our support plans.
        </LeadingText>
        <CycleContainer>
          <CyclePlan
            isActive={cycle === "monthly"}
            onClick={() => setCycle("monthly")}
          >
            Monthly
          </CyclePlan>
          <CyclePlan
            style={{ marginLeft: "0.125rem" }}
            isActive={cycle === "biannualy"}
            onClick={() => setCycle("biannualy")}
          >
            Biannualy
          </CyclePlan>
          <CyclePlan
            style={{ marginLeft: "0.125rem" }}
            isActive={cycle === "annualy"}
            onClick={() => setCycle("annualy")}
          >
            Annualy
          </CyclePlan>
        </CycleContainer>
      </Container>
      <CardContainer>
        <SalesCard
          name="Basic"
          description="The support plan for companies that want to get in contact with a ChilliCream expert
          and prioritised bug fixes for reported incidents."
          price={planPrices.Basic[cycle]}
          cycle="mo"
          perks={[
            "Your own company slack channel, where a maintainer answers your questsions",
            "24 hours response time based on european working days",
            "Two prioritized incidents per month",
          ]}
        />
        <SalesCard
          name="Enterprise"
          description="The support plan for companies that need a GraphQL sparring partner and want a dedicated time pensum for their project per month."
          price={planPrices.Enterprize[cycle]}
          cycle="mo"
          perks={[
            "Your own company slack channel, where a maintainer answers your questsions",
            "24 hours response time based on european working days",
            "Two prioritized incidents per month",
            "12 hours per month at your free disposal<br>&nbsp;&nbsp;- Workshop<br>&nbsp;&nbsp;- Consulting<br>&nbsp;&nbsp;- Code Reviews<br>&nbsp;&nbsp;- Invest the time into a feature of your choice",
          ]}
        />
      </CardContainer>
    </>
  );
};

const Container = styled.div`
  @media (min-width: 640px) {
    display: flex;
    flex-direction: column;
  }
`;

const Title = styled.h1`
  margin: 0;
  color: rgb(17, 24, 39);
  font-size: 3rem;
  line-height: 1;
  font-weight: 800;

  @media (min-width: 640px) {
    text-align: center;
  }
`;

const LeadingText = styled.p`
  margin: 0;
  margin-top: 1.25rem;
  font-size: 1.25rem;
  line-height: 1.75rem;

  color: rgb(107, 114, 128);

  @media (min-width: 640px) {
    text-align: center;
  }
`;

interface CyclePlanpRrops {
  readonly isActive: boolean;
}

const CyclePlan = styled.button<CyclePlanpRrops>`
  padding: 0;
  padding-top: 0.5rem;
  padding-bottom: 0.5rem;
  cursor: pointer;
  color: rgb(55, 65, 81);

  font-size: 0.875rem;
  line-height: 1.25rem;
  font-weight: 500;
  border-radius: 0.375rem;
  border-color: rgb(229, 231, 235);
  background-color: ${(p) => (p.isActive ? "rgb(255, 255, 255)" : "inherit")};
  box-shadow: ${(p) =>
    p.isActive
      ? "0 0 #0000, 0 0 #0000, 0 1px 2px 0 rgba(0, 0, 0, 0.05)"
      : "none"};

  @media (min-width: 640px) {
    padding-left: 2rem;
    padding-right: 2rem;
  }
`;

const CycleContainer = styled.div`
  display: flex;
  align-self: center;
  border-radius: 0.5rem;

  padding: 0.125rem;
  margin-top: 1.5rem;

  background-color: rgb(243, 244, 246);

  ${CyclePlan} {
    width: 50%;
  }

  @media (min-width: 640px) {
    ${CyclePlan} {
      width: auto;
    }
  }
`;

const CardContainer = styled.div`
  margin-top: 3rem;

  > :not(:first-child) {
    margin-top: 16px;
  }

  @media (min-width: 640px) {
    margin-top: 4rem;

    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));

    max-width: 56rem;
    margin-left: auto;
    margin-right: auto;

    gap: 1.5rem;

    > div {
      margin-top: 0 !important;
    }
  }
`;
