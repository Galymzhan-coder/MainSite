import Quill from 'quill';
import hljs from 'highlight.js/lib/core';
import javascript from 'highlight.js/lib/languages/javascript';
import html from 'highlight.js/lib/languages/xml'; // Используется для HTML

hljs.registerLanguage('javascript', javascript);
hljs.registerLanguage('html', html);

const SyntaxHighlight = Quill.import('blots/code');
SyntaxHighlight.tagName = 'pre';

class HighlightBlot extends SyntaxHighlight {
  static create(value) {
    let node = super.create();
    node.innerHTML = value;
    return node;
  }

  static formats(node) {
    return node.innerHTML;
  }

  format(name, value) {
    if (name === 'code' && value) {
      this.domNode.innerHTML = hljs.highlightAuto(this.domNode.innerHTML).value;
    } else {
      super.format(name, value);
    }
  }
}

Quill.register('formats/code', HighlightBlot);
