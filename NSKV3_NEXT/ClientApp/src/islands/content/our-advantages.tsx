type OurAdvantagesProps = {
  items: {
    img: string;
    text: string;
  }[];
};

export default function OurAdvantages({ items }: OurAdvantagesProps) {
  return (
    <div>
      {items?.map((item, index) => (
        <div className="w-1/4" key={index}>
          {/* {item.img} */}
          {/* {item.text} */}
        </div>
      ))}
    </div>
  );
}
