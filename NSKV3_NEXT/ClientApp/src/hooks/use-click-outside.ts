import { LegacyRef, useEffect, useRef } from "react";

export const useClickOutside = (calback): LegacyRef<HTMLElement> => {
  const ref = useRef<HTMLElement>(null);

  const handleEvent = (e) => {
    if (ref.current && !ref.current.contains(e.target)) {
      calback(false);
    }
  };

  useEffect(() => {
    if (window.PointerEvent) {
      document.addEventListener("pointerdown", handleEvent);
    } else {
      document.addEventListener("mousedown", handleEvent);
      document.addEventListener("touchstart", handleEvent);
    }

    return () => {
      if (window.PointerEvent) {
        document.removeEventListener("pointerdown", handleEvent);
      } else {
        document.removeEventListener("mousedown", handleEvent);
        document.removeEventListener("touchstart", handleEvent);
      }
    };
  }, []);

  return ref;
};
