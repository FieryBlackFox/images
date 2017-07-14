import { ImageStorePage } from './app.po';

describe('image-store App', () => {
  let page: ImageStorePage;

  beforeEach(() => {
    page = new ImageStorePage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
