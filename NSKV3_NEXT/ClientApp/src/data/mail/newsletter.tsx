type NewsletterWelcomeEmailProps = {
  firstName?: string;
  fromEmail: string;
  token: string;
};

const baseUrl = process.env.NEXT_PUBLIC_APP_URL ?? "";

export default function NewsletterWelcomeEmail({
  firstName = "there",
  fromEmail,
  token,
}: NewsletterWelcomeEmailProps) {
  const previewText = `Hi ${firstName}, welcome to NSK!`;

  return (
    <>
      <div></div>
    </>
  );
}
